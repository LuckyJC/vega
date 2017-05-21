namespace vega.Controllers.Resources
{
    public class ModelResource
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        //removed inverse relationship to Make class- this was causing a loop in json object
    }
}