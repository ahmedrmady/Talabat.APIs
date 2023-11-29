namespace Talabat.APIs.Errors
{
    public class ApiValidationsErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationsErrorResponse():base(400)
        {
                Errors = new List<string>();
        }

    }
}
