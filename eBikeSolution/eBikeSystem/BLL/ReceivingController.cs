using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBike.Data.POCOs;
using eBikeSystem.DAL;
using eBike.Data.DTOs;
using eBike.Data.Entities;

namespace eBikeSystem.BLL
{
    [DataObject]
    public class ReceivingController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public List<OutstandingOrdersPOCO> GetOutstandingPO()
        {
            using (var context = new eBikeContext())
            {
                var results = from po in context.PurchaseOrders
                              where po.Closed == false && po.OrderDate.HasValue && po.PurchaseOrderNumber.HasValue
                              select new OutstandingOrdersPOCO
                              {
                                  PurchaseOrderID = po.PurchaseOrderID,
                                  PurchaseOrderNumber = po.PurchaseOrderNumber,
                                  OrderDate = po.OrderDate,
                                  Vendor = po.Vendor.VendorName,
                                  Phone = po.Vendor.Phone
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public VendorPurchaseOrderDetailsDTO GetPODetails(int poID)
        {
            using (var context = new eBikeContext())
            {
                VendorPurchaseOrderDetailsDTO vendorPODetails = new VendorPurchaseOrderDetailsDTO();
                List<PurchaseOrderDetailsPOCO> poDetails = new List<PurchaseOrderDetailsPOCO>();

                var poResults = from pod in context.PurchaseOrderDetails
                                orderby pod.PartID ascending
                                where pod.PurchaseOrderID.Equals(poID)
                                select new PurchaseOrderDetailsPOCO
                                {
                                    PurchaseOrderID = pod.PurchaseOrderID,
                                    PurchaseOrderDetailID = pod.PurchaseOrderDetailID,
                                    PartID = pod.PartID,
                                    Description = pod.Part.Description,
                                    QuantityOnOrder = pod.Quantity,
                                    QuantityOutstanding = pod.ReceiveOrderDetails.Select(rod => rod.QuantityReceived).Any() ? pod.Quantity - pod.ReceiveOrderDetails.Sum(rod => rod.QuantityReceived) : pod.Quantity,
                                };
                poDetails = poResults.ToList();

                var vendorResults = (from po in context.PurchaseOrders
                                     where po.PurchaseOrderID.Equals(poID)
                                     select new
                                     {
                                         poNum = po.PurchaseOrderNumber,
                                         Phone = po.Vendor.Phone,
                                         Name = po.Vendor.VendorName
                                     }).FirstOrDefault();  //return first record from return result(using it when have only one item to return) 

                vendorPODetails.PurchaseOrderNumber = vendorResults.poNum;
                vendorPODetails.VendorPhone = vendorResults.Phone;
                vendorPODetails.VendorName = vendorResults.Name;

                vendorPODetails.PODetails = poDetails;

                return vendorPODetails;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void ForceCloser_Update(PurchaseOrder closePOData, List<PurchaseOrderDetailsPOCO> closingPODetailsData)
        {
            using (var context = new eBikeContext())
            {
                var result = context.PurchaseOrders.SingleOrDefault(po => po.PurchaseOrderID == closePOData.PurchaseOrderID);

                if (result != null)
                {
                    result.Notes = closePOData.Notes;
                    result.Closed = closePOData.Closed;
                    //get data from listing for future update
                    foreach (PurchaseOrderDetailsPOCO item in closingPODetailsData)
                    {
                        var checkPartExists = (from p in context.Parts
                                               where p.PartID == item.PartID
                                               select p).SingleOrDefault();
                        if (checkPartExists != null)
                        {   //make sure that qnt on order is not negative number
                            if (checkPartExists.QuantityOnOrder >= item.QuantityOutstanding)
                            {
                                checkPartExists.QuantityOnHand -= item.QuantityOutstanding;
                            }
                            else
                            {
                                throw new Exception("The quantity on order is less than outstanding quantity.");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry there is no such part number in database.");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Add_ReceivedOrders(List<NewReceiveOrderPOCO> receiveNewOrders)
        {
            using (var context = new eBikeContext())
            {
                int id = 0; //this int will hold future PK for update;
                ReceiveOrder receivedOrdersData = new ReceiveOrder();
                ReceiveOrderDetail receivedOrdersDetailsData = null;
                ReturnedOrderDetail returnOrdersDetailsData = null;

                receivedOrdersData.PurchaseOrderID = receiveNewOrders[0].PurchaseOrderID;
                receivedOrdersData.ReceiveDate = DateTime.Now;
                receivedOrdersData = context.ReceiveOrders.Add(receivedOrdersData);
                id = receivedOrdersData.ReceiveOrderDetails.Count() + 1;

                foreach (NewReceiveOrderPOCO item in receiveNewOrders)
                {
                    if (item.QuantityReceived != 0)
                    {   //Part table
                        if (item.QuantityReceived <= item.Outstanding)
                        {
                            receivedOrdersDetailsData = new ReceiveOrderDetail();
                            receivedOrdersDetailsData.PurchaseOrderDetailID = item.PurchaseOrderDetailID;
                            receivedOrdersDetailsData.ReceiveOrderID = id;
                            receivedOrdersDetailsData.QuantityReceived = item.QuantityReceived;

                            receivedOrdersData.ReceiveOrderDetails.Add(receivedOrdersDetailsData);

                            var checkPartExists = (from p in context.Parts
                                                   where p.PartID == item.PartID
                                                   select p).SingleOrDefault();
                            if (checkPartExists != null)
                            {
                                if (checkPartExists.QuantityOnOrder >= item.Outstanding)
                                {
                                    checkPartExists.QuantityOnHand += item.QuantityReceived;
                                    checkPartExists.QuantityOnOrder -= item.QuantityReceived;
                                }
                                else
                                {
                                    throw new Exception("The quantity on order is less than outstanding quantity.");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry there is no such part number in database.");
                            }
                        }
                        else
                        {
                            throw new Exception("The received quantity can't be more than outstanding.");
                        }
                    }
                    //ReturnOrderDetails Table
                    if (!string.IsNullOrEmpty(item.QuantityReturned.ToString()) && !string.IsNullOrEmpty(item.Notes))
                    {
                        returnOrdersDetailsData = new ReturnedOrderDetail();
                        returnOrdersDetailsData.ReceiveOrderID = id;
                        returnOrdersDetailsData.PurchaseOrderDetailID = item.PurchaseOrderDetailID;
                        returnOrdersDetailsData.ItemDescription = item.PartDescription;
                        returnOrdersDetailsData.Quantity = item.QuantityReturned;
                        returnOrdersDetailsData.Reason = item.Notes;
                        returnOrdersDetailsData.VendorPartNumber = item.PartID.ToString();

                        receivedOrdersData.ReturnedOrderDetails.Add(returnOrdersDetailsData);
                    }
                }
                int outstanding = receiveNewOrders.Sum(item => item.Outstanding);
                int received = receiveNewOrders.Sum(item => item.QuantityReceived);

                if ((outstanding - received) == 0)
                {
                    PurchaseOrder poData = context.PurchaseOrders.Find(receiveNewOrders[0].PurchaseOrderID);

                    if (poData != null)
                    {
                        poData.Closed = true;
                    }
                }
                context.SaveChanges();
                
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnorderedPurchaseItemCart> GetUnorderedVendorParts(int poNumber)
        {
            using (var context = new eBikeContext())
            {
                //UnorderedPurchaseItemCart unorderedVendorParts = new UnorderedPurchaseItemCart();

                var results = from upic in context.UnorderedPurchaseItemCarts
                               where upic.PurchaseOrderNumber == poNumber
                               select upic;
                             
                return results.ToList();
            }
        }
        //Insert for UnorderedPurchaseItemCart
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
         public int Add_UnorderedVendorPart(UnorderedPurchaseItemCart itemAdd)
        {
            using (var context = new eBikeContext())
            {
                //itemAdd.PurchaseOrderNumber = poNumber;
                itemAdd = context.UnorderedPurchaseItemCarts.Add(itemAdd);
                context.SaveChanges();

                return itemAdd.CartID;
            }
        }
        //Update for UnorderedPurchaseItemCart
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Remove_UnorderedVendorPart(UnorderedPurchaseItemCart itemDelete)
        {
            using (var context = new eBikeContext())
            {
                var item = context.UnorderedPurchaseItemCarts.Find(itemDelete.CartID);
                context.UnorderedPurchaseItemCarts.Remove(item);
                return context.SaveChanges();              
            }
        }
    }
}
