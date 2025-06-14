using System.Collections.Generic;
using System.Threading.Tasks;
using Video90s.API.Dtos;

namespace Video90s.API.Services
{
    public interface IExternalService
    {
        /// <summary>
        /// Busca shows por título en TVMaze.
        /// </summary>
        Task<IEnumerable<ShowDto>> SearchShowsAsync(string title);
    }
}
