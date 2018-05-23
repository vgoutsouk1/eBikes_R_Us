using eBike.Data.DTOs;
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
    public class JobController
    {//method for the list of current jobs on first page
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobListPoco> JobList()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Jobs
                              where string.IsNullOrEmpty(x.JobDateOut.ToString())
                              select new JobListPoco
                              {
                                  JobID = x.JobID,
                                  JobDateIn = x.JobDateIn,
                                  JobDateStarted = x.JobDateStarted,
                                  JobDateDone = x.JobDateDone,
                                  Name = x.Customer.LastName + ", " + x.Customer.FirstName,
                                  ContactPhone = x.Customer.ContactPhone

                              };
                return results.ToList();
            }
        }//eom

       //method to load the ddl of customers
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<customerPOCO> customerList()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Customers
                              select new customerPOCO
                              {
                                  CustomerID = x.CustomerID,
                                  FirstName = x.FirstName,
                                  LastName = x.LastName
                              };

                return results.ToList();

            }

        }//eom

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Employee> Get_Employees()
        {
            using (var context = new eBikeContext())
            {
                var results = context.Employees.ToList();

                return results;
            }
        }

        //method for the ddl of presets
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StandardJob> presets()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.StandardJobs
                              select x;

                return results.ToList();

            }
        }//eom

        //method for the ddl of coupons
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Coupon> coupon()
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.Coupons
                              select x;

                return results.ToList();

            }
        }//eom


        //method to populate the current jobs services
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobDetail> CurrentJobDetail(int jobid)
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.JobDetails
                              where x.JobID.Equals(jobid)
                              select x;

                return results.ToList();

            }
        }//eom

        //method to populate the current jobs services number 2 (for managing)
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobJobDetails> JobDetailManage(int jobid)
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.JobDetails
                              where x.JobID.Equals(jobid)
                              select new JobJobDetails
                              {
                                  JobDetailID = x.JobDetailID,
                                  Description = x.Description,
                                  StatusCode = x.Job.StatusCode
                              };

                return results.ToList();

            }
        }//eom

        ////method to load a list of Parts associated with a jobdetailid
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<Part> ServiceParts(int jobid)
        //{
        //    using (var context = new eBikeContext())
        //    {
        //        var results = from x in context.JobDetailParts
        //                      where x.JobDetailID == jobid
        //                      select x.Part;

        //        return results.ToList();

        //    }
        //}//eom



        //method to load a list of Parts associated with a jobdetailid
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobPartDTO> ServiceParts(int jobid)
        {
            using (var context = new eBikeContext())
            {
                var results = from x in context.JobDetailParts
                              where x.JobDetailID == jobid
                              select new JobPartDTO
                              {
                                  PartID = x.Part.PartID,
                                  Description = x.Part.Description,
                                  Quantity = x.Quantity,

                              };

                return results.ToList();

            }
        }//eom


        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Job_Add(Job item)
        {
            using (var context = new eBikeContext())
            {
                item = context.Jobs.Add(item);    //staging
                context.SaveChanges();              //commit of the request
                return item.JobID;
            }
        }


        public StandardJob GetPresetsById(int presetid)
        {
            using (var context = new eBikeContext())
            {
                //var results = from x in context.StandardJobs
                //              where x.StandardJobID.Equals(presetid)
                //              select new StandardJob
                //              {
                //                  StandardJobID = x.StandardJobID,
                //                  Description = x.Description,
                //                  StandardHours = x.StandardHours
                //              };

                var results = context.StandardJobs.SingleOrDefault(s => s.StandardJobID.Equals(presetid));

                return results;
            }

        }//eom
    }
}
