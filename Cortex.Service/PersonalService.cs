using Cortex.Core;
using Cortex.Data;
using Cortex.Data.Context;
using Cortex.Domain.Entity;
using Cortex.Domain.WebUI;
using Cortex.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cortex.Service
{
   public class PersonalService
    {
        public ServiceResultModel<PersonalVM> GetPersonalId(int id)
        {
            if (id <= 0)
            { 
                return null;
            }

            PersonalVM currentItem = null;
            using (EFCortexContext context = new EFCortexContext())
            {
                currentItem = context.Personals.FirstOrDefault(p => p.Id == id).MapToViewModel<PersonalVM>();
            }

            return ServiceResultModel<PersonalVM>.OK(currentItem);
        }


        public ServiceResultModel<List<PersonalVM >> GetPersonals()
        {
            List<PersonalVM> resultList = new List<PersonalVM>();

            using (var context = new EFCortexContext())
            {
                IEnumerable<Personal> personalList = context.Personals;
                personalList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapToViewModel<PersonalVM>());
                });
                return ServiceResultModel<List<PersonalVM>>.OK(resultList);
            }
        }
        public ServiceResultModel<PersonalVM> SavePersonal(PersonalVM model)
        {
            using (EFCortexContext context = new EFCortexContext())
            {
                bool isAlreadyExists = context.Personals.Any(p => p.name == model.name);
                if (isAlreadyExists)
                {
                    return new ServiceResultModel<PersonalVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = null,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exists"
                    };
                }

                var recordItem = context.Personals.Add(model.MapToEntityModel<Personal>());
                context.SaveChanges();

                return ServiceResultModel<PersonalVM>.OK(recordItem.MapToViewModel<PersonalVM>());
            }
        }

        public ServiceResultModel<PersonalVM> UpdateHotelType(PersonalVM model)
        {
            using (EFCortexContext context = new EFCortexContext())
            {
                var currentItem = context.Personals.FirstOrDefault(p => p.Id == model.Id);
                if (currentItem != null)
                {
                    // mevcut kayıt haricinde title ile aynı kayıt olamaz kontrol ediyoruz
                    if (context.Personals.Any(p => p.Id != model.Id && p.name.Equals(model.name)))
                    {
                        return new ServiceResultModel<PersonalVM>
                        {
                            Code = ServiceResultCode.Duplicate,
                            Data = currentItem.MapToViewModel<PersonalVM>(),
                            ResultType = OperationResultType.Warn,
                            Message = "This title using other records "
                        };
                    }
                    currentItem.name = model.name;
                    currentItem.task = model.task;
                    currentItem.password = model.password;
                    currentItem.phone = model.phone;
                    currentItem.email = model.email;
                    currentItem.department = model.department;
                    context.Entry<Personal>(currentItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return ServiceResultModel<PersonalVM>.OK(currentItem.MapToViewModel<PersonalVM>());
            }
        }

        public ServiceResultModel<PersonalVM> DeletePersonal(int id)
        {
            using (EFCortexContext context = new EFCortexContext())
            {
                var deleteItem = context.Personals.FirstOrDefault(p => p.Id == id);
                context.Personals.Remove(deleteItem);
                context.SaveChanges();

                return ServiceResultModel<PersonalVM>.OK(deleteItem.MapToViewModel<PersonalVM>());
                /*
                 veya bu şeklide de yazabilirsiniz.
                 EF Tracking sistemi ile çalışır. 2. kodda tracking 'e deleteıtem kaydının Hoteltype tablosunda deleted olarak Track'lendiğini bildiriyoruz.
                 sonrasında commit 'de ilgili kayıt silinir.

                var deleteItem = context.HotelTypes.FirstOrDefault(p => p.Id == id);
                context.Entry<HotelType>(deleteItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();

                 */
            }
        }
    }
}
