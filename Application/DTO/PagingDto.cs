namespace Application.DTO
{
    public class PagingDto
    {
        public int Page { get; set; } = 1;

        public int RecordsPerPage { get; set; } = 2;

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }
    }
}
