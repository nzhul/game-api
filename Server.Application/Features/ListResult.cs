using System.Collections.Generic;

namespace Server.Application.Features
{
    public class ListResult<T>
    {
        public List<T> Items { get; set; }

        public long Count { get; set; }
    }
}
