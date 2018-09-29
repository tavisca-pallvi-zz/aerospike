using Aerospike.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace HotelWebApp.Controllers
{
    public class HotelController : ApiController
    {
        string nameSpace = null;
        string setName = null;
        AerospikeClient aerospikeClient = null;
        public void Connect()
        {
            try
            {
                aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                nameSpace = "AirEngine";
                setName = "Pallvi3";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [System.Web.Http.HttpPost]

        public List<Record> GetMultipleData([FromBody]JObject jsonFormatInput)
        {
            List<Record> output = new List<Record>();
            for (int i = 1; i <= jsonFormatInput.Count; i++)
            {
                string id = jsonFormatInput.GetValue("id" + i).ToString();
                try
                {
                    var key = new Key(nameSpace, setName, id.ToString());
                    Record objects = aerospikeClient.Get(new WritePolicy(), key);
                    output.Add(objects);
                }
                catch (Exception ex)
                {


                    throw ex;
                }

            }

            return output;
        }
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("put/{id}")]
        public void UpdateDataByID(long id, [FromBody]string updatedYear)
        {
            Bin newBin = new Bin("year", updatedYear);
            var key = new Key(nameSpace, setName, id.ToString());
            try
            {
                aerospikeClient.Put(new WritePolicy(), key, newBin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.HttpDelete]
        [Route("delete/{id}")]
        public void DeleteData(long id)
        {
            var key = new Key(nameSpace, setName, id.ToString());
            try
            {
                aerospikeClient.Delete(new WritePolicy(), key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }




}
