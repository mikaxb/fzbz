using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text;

namespace FzBz.Domain;

public interface IFzBzService
{
    IDictionary<int, string> GetResult(int[] numbers);
    Task<IDictionary<int, string>> GetResultAsync(int[] numbers);
}

internal class FzBzService(ILogger<FzBzService> logger) : IFzBzService
{
    public IDictionary<int, string> GetResult(int[] numbers)
    {
        ConcurrentDictionary<int, string> cdic = new();
        Parallel.ForEach(numbers, number =>
            {
                if(!cdic.TryAdd(number, ResultBuilder(number)))
                {
                    logger.LogInformation("Could not add {number} to result dictionary", number);
                }
            });
        
        return cdic.OrderBy(e => e.Key).ToDictionary();
    }

    public async Task<IDictionary<int, string>> GetResultAsync(int[] numbers)
    {
        throw new NotImplementedException();
    }

    private static string ResultBuilder(int number)
    {
        StringBuilder sb = new();
        if (number % 3 == 0)
        {
            sb.Append(StringDefinitions.FIZZ);
        }
        if (number % 5 == 0)
        {
            sb.Append(StringDefinitions.BUZZ);
        }

        return sb.Length > 0 ? sb.ToString() : number.ToString();
    }
}
