using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using TopSpotsAPI.Models;
using System.Net.Http;
using System.Net;

namespace TopSpotsAPI.Controllers
{
    public class TopSpotsController : ApiController
    {
        // GET: api/TopSpots
        public IEnumerable<TopSpot> Get()
        {
            // Reads in a file and deserializes it
            var jsonArray = JsonConvert.DeserializeObject<IEnumerable<TopSpot>>(File.ReadAllText("C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json"));
            return jsonArray;
        }

        // GET: api/TopSpots/5
        public object Get(int id)
        {
            return "value";
        }

        // POST: api/TopSpots
        public HttpResponseMessage Post([FromBody]TopSpot topspot)
        {
            // reads the topspots.json into a local deserialized array
            var jsonArray = JsonConvert.DeserializeObject<List<TopSpot>>(File.ReadAllText("C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json"));
            
            // adds the object written within the body of the POST message, back into the local deserialized array
            jsonArray.Add(topspot);
            
            // serializes the jsonArray and stores it into another local variable
            var convertedJson = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);

            File.WriteAllText(@"C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json", convertedJson);

            // Sends a Success Http Response message
            return Request.CreateResponse(HttpStatusCode.OK,
            new
            {
                value = topspot,
                message = "success!"
            });
        }

        // PUT: api/TopSpots/5
        public HttpResponseMessage Put(int id, [FromBody]TopSpot topspot)
        {
            // reads the topspots.json into a local deserialized array
            var jsonArray = JsonConvert.DeserializeObject<List<TopSpot>>(File.ReadAllText("C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json"));
            
            //check to see if the name property within the body of the PUT message has contents, if yes then grab that value and store it within the appropriate element inside of the Json array
            if (topspot.name != null)
            {
                jsonArray[id].name = topspot.name;
            }

            //check to see if the description property within the body of the PUT message has contents, if yes then grab that value and store it within the appropriate element inside of the Json array

            if (topspot.description != null)
            {
                jsonArray[id].description = topspot.description;
            }

            //check to see if the location property within the body of the PUT message has contents, if yes then grab that value and store it within the appropriate element inside of the Json array

            if (topspot.location != null)
            {
                jsonArray[id].location[0] = topspot.location[0];
                jsonArray[id].location[1] = topspot.location[1];
            }

            // serializes the jsonArray and stores it into another local variable
            var convertedJson = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);

            //writes the serialized JSON back into the external JSON file
            File.WriteAllText(@"C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json", convertedJson);

            // Sends an Success Http Response message
            return Request.CreateResponse(HttpStatusCode.OK,
            new
            {
                value = topspot,
                message = "success!"
            });
        }

        // DELETE: api/TopSpots/5
        public HttpResponseMessage Delete(int id)
        {
            // reads the topspots.json file, deserializes it and then stores it into a local array
            var jsonArray = JsonConvert.DeserializeObject<List<TopSpot>>(File.ReadAllText("C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json"));
            
            // remove the record at id location
            jsonArray.RemoveAt(id);

            // serializes the jsonArray and stores it into another local variable
            var convertedJson = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);

            //writes the serialized JSON back into the external JSON file
            File.WriteAllText(@"C:/dev/github/reuvenkishon/TopSpotsAPI/TopSpotsAPI/TopSpotsAPI/topspots.json", convertedJson);

            // Sends an Success Http Response message
            return Request.CreateResponse(HttpStatusCode.OK,
            new
            {
                message = "success!"
            });
        }
    }
}