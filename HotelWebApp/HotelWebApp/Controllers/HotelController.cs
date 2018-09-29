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
        AerospikeClient aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
        string nameSpace = "AirEngine";
        string setName = "Pallvi3";
        //List<Record>
        [System.Web.Http.HttpPost]
   
        public List<Record> GetMultipleData([FromBody]JObject jsonFormatInput)
        {
            List<Record> output = new List<Record>();
            for (int i = 1; i <= jsonFormatInput.Count; i++)
            {
                string id = jsonFormatInput.GetValue("id" + i).ToString();
                var key = new Key(nameSpace, setName, id.ToString());
                Record objects = aerospikeClient.Get(new WritePolicy(), key);
                output.Add(objects);
            }
            return output;
        }
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("put/{id}")]
        public void UpdateDataByID(long id, [FromBody]string updatedYear)
        {  Bin newBin = new Bin("year", updatedYear);
            var key = new Key(nameSpace, setName, id.ToString());
            aerospikeClient.Put(new WritePolicy(), key, newBin);
       
        }

        [System.Web.Http.HttpDelete]
        [Route("delete/{id}")]
        public void DeleteData(long id)
        {
            var key = new Key(nameSpace, setName, id.ToString());
            aerospikeClient.Delete(new WritePolicy(), key);
        }
    }




}
