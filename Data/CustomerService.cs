using AdoApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoApplication.Data
{
    public class CustomerService
    {

        ConnectDb dB;
        public CustomerService() {
            this.dB = new ConnectDb();

        }
  
        public List<Country> GetCountryList()
        {
            SqlCommand cmd = new SqlCommand("USP_GET_COUNTRY", dB.connect);
            cmd.CommandType=CommandType.StoredProcedure;
            if(dB.connect.State==ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            SqlDataReader dr = cmd.ExecuteReader();

            List<Country> list = new List<Country>();
            while(dr.Read())
            {
               Country country = new Country();
                country.Id = Convert.ToInt32(dr["Id"]);
                country.Name = dr["Name"].ToString();
                list.Add(country);
            }
            dB.connect.Close();
            return list;
        }

        public List<State> GetStateList(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_GET_STATE", dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);

            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            SqlDataReader dr = cmd.ExecuteReader();

            List<State> list = new List<State>();
            while (dr.Read())
            {
                State state = new State();
                state.Id = Convert.ToInt32(dr["ID"]);
                state.Name = dr["NAME"].ToString();
                list.Add(state);
            }
            dB.connect.Close();
            return list;
        }
        public List<City> GetCityList(int stateid)
        {
            SqlCommand cmd = new SqlCommand("USP_GET_CITY", dB.connect);
            cmd.CommandType =CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", stateid);
            if (dB.connect.State ==ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            SqlDataReader dr = cmd.ExecuteReader();

            List<City> list = new List<City>();
            while (dr.Read())
            {
                City city = new City();
                city.Id = Convert.ToInt32(dr["Id"]);
                city.Name = dr["Name"].ToString();
                list.Add(city);
            }
            dB.connect.Close();
            return list;
        }



        public List<Customer> GetCustomers()
        {
            SqlCommand cmd = new SqlCommand("usp_get_customer", dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;

         //   cmd.Parameters.AddWithValue("@Id", stateid);
            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            SqlDataReader dr = cmd.ExecuteReader();

            List<Customer> list = new List<Customer>();
            while (dr.Read())
            {
                Customer customer = new Customer();
                customer.id = Convert.ToInt32(dr["id"]);
                customer.name = dr["name"].ToString();
                customer.email = dr["email"].ToString();
                customer.mobile = dr["mobile"].ToString();
                customer.gender = dr["gender"].ToString();
                customer.country = dr["country"].ToString();
                customer.state = dr["state"].ToString();
                customer.city = dr["city"].ToString();

                list.Add(customer);
            }
            dB.connect.Close();
            return list;
        }

        public Customer GetCustomer(int id)
        {
            SqlCommand cmd = new SqlCommand("usp_get_customerbyid", dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@Id", id);
            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            SqlDataReader dr = cmd.ExecuteReader();

           
           
                Customer customer = new Customer();
                  dr.Read();
                customer.id = Convert.ToInt32(dr["id"]);
                customer.name = dr["name"].ToString();
                customer.email = dr["email"].ToString();
                customer.mobile = dr["mobile"].ToString();
                customer.gender = dr["gender"].ToString();
                customer.country = dr["country"].ToString();
                customer.state = dr["state"].ToString();
                customer.city = dr["city"].ToString();

                
            
            dB.connect.Close();
            return customer;
        }

        public Models.Action CreateCustomers(Customer customer)


        {
            SqlCommand cmd = new SqlCommand("usp_save_customer", dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", customer.name);
            cmd.Parameters.AddWithValue("@email", customer.email);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@country_id", customer.country);
            cmd.Parameters.AddWithValue("@state_id", customer.state);
            cmd.Parameters.AddWithValue("@city_id", customer.city);

            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            int r=(int)cmd.ExecuteScalar();
            Models.Action action;

            if (r==1)
            {
                action = Models.Action.success;
            }
            else if (r == 2)
            {
                action = Models.Action.EmailExist;
            }
            else
            {
                action = Models.Action.error;
            }
            dB.connect.Close();
           
            
            return  action;
        }


        public bool Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("usp_delete_customer", dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;

               cmd.Parameters.AddWithValue("@id", id);
            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            int r=(int)cmd.ExecuteScalar();
            dB.connect.Close();
            if(r==1)
            {
              return true;
            }
            else
            {
                return false;
            }
        }



        [HttpPost]
        public Models.Action Update(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("usp_update_customer1",dB.connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", customer.id);
            cmd.Parameters.AddWithValue("@name", customer.name);
            cmd.Parameters.AddWithValue("@email", customer.email);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@country_id", customer.country);
            cmd.Parameters.AddWithValue("@state_id", customer.state);
            cmd.Parameters.AddWithValue("@city_id", customer.city);

            if (dB.connect.State == ConnectionState.Closed)
            {
                dB.connect.Open();

            }
            int r = (int)cmd.ExecuteScalar();
            Models.Action action;

            if (r == 1)
            {
                action = Models.Action.success;
            }
            else if (r == 2)
            {
                action = Models.Action.EmailExist;
            }
            else
            {
                action = Models.Action.error;
            }
            dB.connect.Close();

            return action;

        }

    }
}
