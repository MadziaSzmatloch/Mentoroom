using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mentoroom.DOMAIN.Entities.LecturerModels
{
    public class AccessCode
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;

        public AccessCode()
        {

        }

        public AccessCode(DateTime expirationDate)
        {
            CreationDate = DateTime.Now;
            ExpirationDate = expirationDate;
        }

        public async Task GenerateUniqueCode(DbContext context)
        {
            var random = new Random();
            var code = random.Next(100000000, 999999999).ToString();

            while (await context.Set<AccessCode>().AnyAsync(x => x.Code == code))
            {
                code = random.Next(100000000, 999999999).ToString();
            }

            this.Code = code;
        }
    }
}
