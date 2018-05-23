using eBike.Data.Entities;
using eBike.Data.POCOs;
using eBikeSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBikeSystem.BLL
{
    [DataObject]
    public class SalesController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryListPOCO> CategoryList()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Categories
                              select new CategoryListPOCO
                              {
                                  CategoryID = x.CategoryID,
                                  Description = x.Description
                              };
                return results.ToList();
            }
        }//CategoryList

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PartsListPOCO> Parts_byCategoryList(int categoryid)
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Parts
                              where x.Category.CategoryID == categoryid && x.Discontinued == false
                              select new PartsListPOCO
                              {
                                  PartID = x.PartID,
                                  Description = x.Description,
                                  InStock = x.QuantityOnHand,
                                  Price = x.SellingPrice
                              };
                return results.ToList();
            }
        }//Parts_byCategoryList

        public void Add_ItemToCart(string username, int partid, int quantity)
        {
            using (var context = new eBikeContext())
            {
                var customer = (from x in context.OnlineCustomers
                                where x.UserName.Equals(username)
                                select x).FirstOrDefault();
                if (customer == null)
                {
                    var sysmgr = new OnlineUsersController();
                    sysmgr.Add_OnlineCustomer(username);

                    customer = (from x in context.OnlineCustomers
                                    where x.UserName.Equals(username)
                                    select x).FirstOrDefault();
                }

                int customerid = customer.OnlineCustomerID;

                var shoppingcart = (from x in context.ShoppingCarts
                                    where x.OnlineCustomerID.Equals(customerid)
                                    select x).FirstOrDefault();

                ShoppingCartItem newitem = null;

                if (shoppingcart == null)
                {
                    //create a new cart
                    shoppingcart = new ShoppingCart();
                    shoppingcart.OnlineCustomerID = customerid;
                    shoppingcart.CreatedOn = DateTime.Now;
                    shoppingcart = context.ShoppingCarts.Add(shoppingcart);
                }
                else
                {
                    //check existing cart for the item

                    newitem = shoppingcart.ShoppingCartItems.SingleOrDefault(x => x.PartID == partid);
                    //this will be null if the item is NOT in the cart
                    if (newitem != null)
                    {
                        throw new Exception("That item is already in your shopping cart.");
                    }
                }
                int shoppingcartid = shoppingcart.ShoppingCartID;
                newitem = new ShoppingCartItem();
                newitem.ShoppingCartID = shoppingcartid;
                newitem.PartID = partid;
                newitem.Quantity = quantity;

                context.ShoppingCartItems.Add(newitem);
                context.SaveChanges();
            }
        }//Add_ItemToCart

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ShoppingCartListPOCO> ShoppingCartList(string username)
        {
            using (var context = new eBikeContext())
            {
                var customer = (from x in context.OnlineCustomers
                                where x.UserName.Equals(username)
                                select x).FirstOrDefault();
                if (customer == null)
                {
                    return new List<ShoppingCartListPOCO>();
                }
                else
                {
                    int customerid = customer.OnlineCustomerID;

                    var results = from x in context.ShoppingCartItems
                                  where x.ShoppingCart.OnlineCustomerID.Equals(customerid)
                                  select new ShoppingCartListPOCO
                                  {
                                      PartID = x.PartID,
                                      Description = x.Part.Description,
                                      Quantity = x.Quantity,
                                      UnitPrice = x.Part.SellingPrice,
                                      TotalPrice = (x.Quantity) * (x.Part.SellingPrice)
                                  };
                    return results.ToList();
                }
            }
        }//ShoppingCartList

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Coupon> SalesCouponList()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Coupons
                              where x.SalesOrService.Equals(1)
                              select x;
                return results.ToList();
            }
        }//SalesCouponList

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TotalPricePOCO ShoppingCart_Totals(string username)
        {
            using (var context = new eBikeContext())
            {
                var totals = new TotalPricePOCO();

                var customer = (from x in context.OnlineCustomers
                                where x.UserName.Equals(username)
                                select x).FirstOrDefault();

                if (customer == null)
                {
                    totals.SubTotal = 0;
                    totals.GST = 0;
                    totals.Total = 0;
                }
                else
                {
                    int customerid = customer.OnlineCustomerID;

                    var shoppingcart = (from x in context.ShoppingCarts
                                        where x.OnlineCustomerID.Equals(customerid)
                                        select x).FirstOrDefault();
                    if (shoppingcart == null)
                    {
                        totals.SubTotal = 0;
                        totals.GST = 0;
                        totals.Total = 0;
                    }
                    else
                    {
                        var items = (from x in context.ShoppingCartItems
                                    where x.ShoppingCartID == shoppingcart.ShoppingCartID
                                    select x).FirstOrDefault();
                        if (items == null)
                        {
                            totals.SubTotal = 0;
                            totals.GST = 0;
                            totals.Total = 0;
                        }
                        else
                        {
                            var sum = (from x in context.ShoppingCartItems
                                       where x.ShoppingCartID == shoppingcart.ShoppingCartID
                                       select (x.Quantity * x.Part.SellingPrice)).Sum();

                            totals.SubTotal = sum;
                            totals.GST = Decimal.Multiply(sum, decimal.Parse("0.05"));
                            totals.Total = totals.SubTotal + totals.GST;
                        }

                        
                    }

                    
                }

                return totals;
            }

                    
        }//ShoppingCartTotals

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FinalTotalPOCO ShoppingCart_FinalTotals(string username, int couponid)
        {
            using (var context = new eBikeContext())
            {
                var totals = new FinalTotalPOCO();

                var customer = (from x in context.OnlineCustomers
                                where x.UserName.Equals(username)
                                select x).FirstOrDefault();

                if (customer == null)
                {
                    totals.SubTotal = 0;
                    totals.Discount = 0;
                    totals.GST = 0;
                    totals.Total = 0;
                }
                else
                {
                    

                    int customerid = customer.OnlineCustomerID;

                    double coupondiscount = (from x in context.Coupons
                                            where x.CouponID == couponid
                                            select x.CouponDiscount).FirstOrDefault();

                    var shoppingcart = (from x in context.ShoppingCarts
                                        where x.OnlineCustomerID.Equals(customerid)
                                        select x).FirstOrDefault();
                    if (shoppingcart == null)
                    {
                        totals.SubTotal = 0;
                        totals.Discount = 0;
                        totals.GST = 0;
                        totals.Total = 0;
                    }
                    else
                    {
                        var items = (from x in context.ShoppingCartItems
                                     where x.ShoppingCartID == shoppingcart.ShoppingCartID
                                     select x).FirstOrDefault();
                        if (items == null)
                        {
                            totals.SubTotal = 0;
                            totals.Discount = 0;
                            totals.GST = 0;
                            totals.Total = 0;
                        }
                        else
                        {
                            var sum = (from x in context.ShoppingCartItems
                                       where x.ShoppingCartID == shoppingcart.ShoppingCartID
                                       select (x.Quantity * x.Part.SellingPrice)).Sum();

                            totals.SubTotal = sum;
                            totals.Discount = Decimal.Multiply(sum, decimal.Parse((coupondiscount / 100).ToString()));
                            totals.GST = Decimal.Multiply((sum - totals.Discount), decimal.Parse("0.05"));
                            totals.Total = totals.SubTotal - totals.Discount + totals.GST;
                        }

                        
                    }

                    
                }

                return totals;
            }
        }//ShoppingCart_FinalTotals

        public void Update_CartItem(string username, int partid, int quantity)
        {
            using (var context = new eBikeContext())
            {
                int customerid = (from x in context.OnlineCustomers
                                where x.UserName.Equals(username)
                                select x.OnlineCustomerID).FirstOrDefault();


                int shoppingcartid = (from x in context.ShoppingCarts
                                    where x.OnlineCustomerID.Equals(customerid)
                                    select x.ShoppingCartID).FirstOrDefault();


                int updateItemID =  (from x in context.ShoppingCartItems
                                  where x.ShoppingCartID.Equals(shoppingcartid) && x.PartID.Equals(partid)
                                  select x.ShoppingCartItemID).FirstOrDefault();

                var updateItem = context.ShoppingCartItems.Find(updateItemID);

                updateItem.Quantity = quantity;

                context.Entry(updateItem).Property(y => y.Quantity).IsModified = true;

                context.SaveChanges();
            }
        }//Update_CartItem

        public void Remove_CartItem(string username, int partid)
        {
            using (var context = new eBikeContext())
            {
                int customerid = (from x in context.OnlineCustomers
                                  where x.UserName.Equals(username)
                                  select x.OnlineCustomerID).FirstOrDefault();


                int shoppingcartid = (from x in context.ShoppingCarts
                                      where x.OnlineCustomerID.Equals(customerid)
                                      select x.ShoppingCartID).FirstOrDefault();

                int shoppingcartitemid = (from x in context.ShoppingCartItems
                              where x.ShoppingCartID.Equals(shoppingcartid) && x.PartID.Equals(partid)
                              select x.ShoppingCartItemID).FirstOrDefault();

                var existingItem = context.ShoppingCartItems.Find(shoppingcartitemid);

                context.ShoppingCartItems.Remove(existingItem);

                context.SaveChanges();

            }
        }//Remove_CartItem

        public List<Part> Check_ForBackorders(string username)
        {
            using (var context = new eBikeContext())
            {
                List<Part> backordered = new List<Part>();

                //Get the customers id
                int customerid = (from x in context.OnlineCustomers
                                  where x.UserName.Equals(username)
                                  select x.OnlineCustomerID).FirstOrDefault();
                if (customerid != 0)
                {
                    //Get the customers shopping cart id
                    int shoppingcartid = (from x in context.ShoppingCarts
                                          where x.OnlineCustomerID.Equals(customerid)
                                          select x.ShoppingCartID).FirstOrDefault();

                    if (shoppingcartid != 0)
                    {
                        //Get a list of all items in the customers shopping cart
                        List<ShoppingCartItem> useritems = (from x in context.ShoppingCartItems
                                                            where x.ShoppingCart.ShoppingCartID.Equals(shoppingcartid)
                                                            select x).ToList();
                        if (useritems.Count > 0)
                        {
                            foreach (ShoppingCartItem item in useritems)
                            {
                                Part part = (from x in context.Parts
                                             where x.PartID.Equals(item.PartID)
                                             select x).FirstOrDefault();

                                if (item.Quantity > part.QuantityOnHand)
                                {
                                    backordered.Add(part);
                                }
                            }
                        }
                    }
                }

                return backordered;
            }
        }//Check_ForBackorders

        public List<Part> Place_Order(string username, int couponid, FinalTotalPOCO totals, string paymethod)
        {
            using (var context = new eBikeContext())
            {
                //Get the customers id
                int customerid = (from x in context.OnlineCustomers
                                  where x.UserName.Equals(username)
                                  select x.OnlineCustomerID).FirstOrDefault();
                if (customerid == 0)
                {
                    throw new Exception("You do not have a shopping cart, please add items to place an order");
                }
                else
                {
                    //Create and populate a new Sale
                    Sale newsale = new Sale();
                    newsale.SaleDate = DateTime.Now;
                    newsale.UserName = username;
                    newsale.EmployeeID = 301;
                    newsale.TaxAmount = totals.GST;
                    newsale.SubTotal = totals.SubTotal;
                    //Check if a coupon has been selected (FK constraint)
                    if (couponid == 0)
                    {
                        newsale.CouponID = null;
                    }
                    else
                    {
                        newsale.CouponID = couponid;
                    }                   
                    newsale.PaymentType = paymethod;
                    newsale.PaymentToken = Guid.NewGuid();

                    //Add the new Sale to database
                    newsale = context.Sales.Add(newsale);

                    //Get the customers shopping cart id
                    int shoppingcartid = (from x in context.ShoppingCarts
                                          where x.OnlineCustomerID.Equals(customerid)
                                          select x.ShoppingCartID).FirstOrDefault();

                    //Get a list of all items in the customers shopping cart
                    List<ShoppingCartItem> useritems = (from x in context.ShoppingCartItems
                                                        where x.ShoppingCart.ShoppingCartID.Equals(shoppingcartid)
                                                        select x).ToList();

                    //Create a list of parts to be filled with backordered parts
                    List<Part> backorderedparts = new List<Part>();

                    //Create a Sale Detail for each item
                    foreach (ShoppingCartItem item in useritems)
                    {
                        //Get the corresponding part info
                        Part part = (from x in context.Parts
                                    where x.PartID.Equals(item.PartID)
                                    select x).FirstOrDefault();

                        //Check the remaining quantity of the part
                        if (item.Quantity > part.QuantityOnHand)
                        {
                            //Create a backordered sale detail
                            SaleDetail boitem = new SaleDetail();
                            boitem.SaleID = newsale.SaleID;
                            boitem.PartID = part.PartID;
                            boitem.Quantity = item.Quantity;
                            boitem.SellingPrice = part.SellingPrice;
                            boitem.Backordered = true;
                            boitem.ShippedDate = null;

                            //Add the backordered part to return list
                            backorderedparts.Add(part);

                            //Add the backordered sale detail to database
                            context.SaleDetails.Add(boitem);

                        }
                        else
                        {
                            //Create a regular sale detail
                            SaleDetail saleitem = new SaleDetail();
                            saleitem.SaleID = newsale.SaleID;
                            saleitem.PartID = part.PartID;
                            saleitem.Quantity = item.Quantity;
                            saleitem.SellingPrice = part.SellingPrice;
                            saleitem.Backordered = false;
                            saleitem.ShippedDate = DateTime.Now;

                            //Update QuantityOnHand for the part
                            part.QuantityOnHand = part.QuantityOnHand - item.Quantity;

                            //Make change to QOH in the database
                            context.Entry(part).Property(y => y.QuantityOnHand).IsModified = true;

                            //Add the new sale detail
                            context.SaleDetails.Add(saleitem);

                        }

                        //Delete the ShoppingCartItem from users ShoppingCart
                        context.ShoppingCartItems.Remove(item);

                    }//foreach item in useritems

                    //Find and delete the users ShoppingCart
                    var existingItem = context.ShoppingCarts.Find(shoppingcartid);
                    context.ShoppingCarts.Remove(existingItem);

                    //Save changes
                    context.SaveChanges();

                    //Return any backordered parts for display to user
                    return backorderedparts;
                }
                
            }
        }//Place_Order

    }
}
