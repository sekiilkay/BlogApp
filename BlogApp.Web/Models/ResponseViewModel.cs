namespace BlogApp.Web.Models
{
    public class ResponseViewModel
    {
        public List<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }

        public static ResponseViewModel Success()
        {
            return new ResponseViewModel
            {
                Errors = null,
                IsSuccess = true
            };
        }

        public static ResponseViewModel Fail(string error)
        {
            return new ResponseViewModel
            {
                Errors = new List<string> { error },
                IsSuccess = false
            };
        }

        public static ResponseViewModel Fail(List<string> errors)
        {
            return new ResponseViewModel
            {
                Errors = errors ?? new List<string> { "Bilinmeyen hata." },
                IsSuccess = false
            };
        }
    }
}