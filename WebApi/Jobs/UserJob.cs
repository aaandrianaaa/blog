using Quartz;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Jobs
{
    public class UserJob : IJob
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        public UserJob(IUserRepository userRepository, IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await UpdateUserAge();
            await UpdateUserRating();
        }

        private async Task UpdateUserAge()
        {
            Debug.WriteLine("UPDATE USER AGE");
            var users = await _userRepository.GetListAsync(x => x.BirthdayDate.Value.DayOfYear == DateTime.Now.DayOfYear && x.DeletedAt == null);
            foreach (var user in users)
            {
                user.Age++;
            }

            await _userRepository.SaveAsync();
        }

        private async Task UpdateUserRating()
        {
            var users = await _userRepository.GetListAsync(x => x.DeletedAt == null);
            foreach (var user in users)
            {
                var average = (await _articleRepository.GetListAsync(x => x.AuthorID == user.ID)).Select(x => x.Rating).Average();
                user.Rating = average;
            }

            await _userRepository.SaveAsync();
        }
    }
}
