namespace BuildingBlocks.Exceptions
{
    internal class InternalServerError : Exception
    {
        public string? Details { get; }

        public InternalServerError(string message) : base(message) { }

        public InternalServerError(string message, string details) : base(message) 
        {
            Details = details;
        }

    }
}
