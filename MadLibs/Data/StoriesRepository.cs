using Dapper;
using MadLibs.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace MadLibs.Data
{
    public class StoriesRepository : IStoriesRepository
    {
        private readonly string _connectionString;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public StoriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task MethodTesting()
        {
            long totalAsyncTime = 0;
            long totalAltTime = 0;
            using var connection = CreateConnection();

            for(int i = 0; i < 10; i++)
            {
                stopwatch.Restart();
                stopwatch.Start();
                await GetStoriesViewModelsAsync();  //Slower because of in-memory grouping in LINQ which adds computational cost.
                stopwatch.Stop();
                long elapsedAsync = stopwatch.ElapsedMilliseconds;
                totalAsyncTime += elapsedAsync;
                Console.WriteLine($"Run {i + 1}: GetStoriesViewModelsAsync took {elapsedAsync} ms");
            }

            for(int i = 0; i < 10; i++)
            {
                stopwatch.Restart();
                stopwatch.Start();
                await GetStoriesViewModelsAlt();
                stopwatch.Stop();
                long elapsedAlt = stopwatch.ElapsedMilliseconds;
                totalAltTime += elapsedAlt;
                Console.WriteLine($"Run {i + 1}: GetStoriesViewModelsAlt took {elapsedAlt} ms");
            }

            long averageAsyncTime = totalAsyncTime / 10;
            long averageAltTime = totalAltTime / 10;

            Console.WriteLine($"Average time for GetStoriesViewModelsAsync: {averageAsyncTime} ms");
            Console.WriteLine($"Average time for GetStoriesViewModels_Alt: {averageAltTime} ms");
        }


        public async Task<List<StoryViewModel>> GetStoriesViewModelsAsync()
        {
            var sql = "GetStoriesViewModels"; // Stored procedure name
            using var connection = CreateConnection();
            // Execute the stored procedure and map the results to StoryViewModel, Placeholder, and UserResponse then output to StoryViewModel
            var queryResult = await connection.QueryAsync<StoryViewModel, Placeholder, UserResponse, StoryViewModel>(
                sql,
                (viewModel, placeholder, response) =>  // Map the results to StoryViewModel, Placeholder, and UserResponse
                {
                    if(placeholder != null)
                        viewModel.Placeholders.Add(placeholder);

                    if(response != null)
                        viewModel.Responses.Add(response);

                    return viewModel;
                },
                commandType: CommandType.StoredProcedure
            );  //produces duplicate stories for each placeholder and response

            // Group the results by StoryViewModel Id to consolidate the data
            var results = queryResult.GroupBy(q => q.Id)
                .Select(g =>
                {
                    var story = g.First(); 
                    story.Id = g.First().Id;
                    story.Title = g.First().Title;
                    story.Template = g.First().Template;
                    story.ImagePath = g.First().ImagePath;
                    story.IsExpanded = g.First().IsExpanded;

                    // Combine all responses and placeholders from the grouped results
                    story.Responses = g.SelectMany(q => q.Responses).ToList();
                    story.Placeholders = g.SelectMany(q => q.Placeholders).ToList();

                    return story; 
                });
            return results.ToList(); 
        }

        public async Task<List<StoryViewModel>> GetStoriesViewModelsAlt()
        {
            var storiesSp = "GetAllStoriesThemes";
            var placeholdersSp = "GetAllPlaceholders";
            var responsesSp = "GetAllResponses";
            var storiesViewModels = new List<StoryViewModel>();
            var placeholders = new List<Placeholder>();
            var responses = new List<UserResponse>();
            using var connection = CreateConnection();
            // get all stories traditional and with using reader:
            storiesViewModels =  (await connection.QueryAsync<StoryViewModel>(storiesSp, commandType: CommandType.StoredProcedure)).ToList();
            placeholders = (await connection.QueryAsync<Placeholder>(placeholdersSp, commandType: CommandType.StoredProcedure)).ToList();
            responses = (await connection.QueryAsync<UserResponse>(responsesSp, commandType: CommandType.StoredProcedure)).ToList();
            foreach(var vm in storiesViewModels)
            {
                vm.Placeholders = placeholders.Where(p => p.StoryId == vm.Id).ToList();
                vm.Responses = responses.Where(r => r.StoryId == vm.Id).ToList();
            }

            return storiesViewModels;
        }

    }
}

