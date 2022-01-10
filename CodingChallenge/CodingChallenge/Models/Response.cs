namespace CodingChallengeWeb.Models
{
    public class Response
    {
        public string name { get; set; }
        public double p { get; set; }
        public Response(string name, double p)
        {
            this.name = name;
            this.p = p;
        }
    }
}
