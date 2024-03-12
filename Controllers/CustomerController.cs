using AdoApplication.Data;
using AdoApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdoApplication.Controllers
{
    public class CustomerController : Controller
    {

        CustomerService service=new CustomerService();
        public IActionResult Create()
        {
            return View();
        }

       
        public IActionResult DeleteCustomer(int id)
        {
            var result=service.Delete(id);
            if (result)
            {
                return Json(new { ok = true, message = "customer delete successfully" });

            }
            else
            {
                return Json(new { ok = false, message = "customer not delete successfully" });

            }
            
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            var result=service.CreateCustomers(customer);
            if(result==Models.Action.success)
            {
                return Json(new { ok = true, message = "customer create successfully" });
            }
           else if (result == Models.Action.EmailExist)
            {
                return Json(new { ok = false, message = "customer email already ragister" });

            }
            else
            {
                return Json(new { ok = false, message = "something went wrong" });

            }
           
        }


        public IActionResult GetCustomers()
        {
            return Json(service.GetCustomers());
        }


        
        public IActionResult GetCustomer(int id)
        {
            return Json(service.GetCustomer(id));
        }

        public IActionResult GetCountry()
        {
            return Json(service.GetCountryList());
        }

        public IActionResult GetState(int id)
        {
            return Json(service.GetStateList(id));
        }

        public IActionResult GetCity(int id)
        {
            return Json(service.GetCityList(id));
        }



        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var result=service.Update(customer);
            if (result == Models.Action.success)
            {
                return Json(new { ok = true, message = "customer update succefuuly" });

            }
            else if(result == Models.Action.EmailExist)
            {
                return Json(new { ok = false, message = "customer not exist" });

            }
            else
            {
                return Json(new { ok = false, message = "something went wrong" });

            }
        }
       

        

    }
}
