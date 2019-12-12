using Cortex.Core.CustomAttribute;
using Cortex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Cortex.Service.Extensions
{
  public static class EntityMamperExtensions
    {
        private static void MatchProp<T , K>(this T source, K destination)
            where T : IDomain 
            where K : IDomain
        {
            if (source == null || destination == null)
            {
                return;
            }
            List<PropertyInfo> sourceProp = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProp = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProp)
            {
                PropertyInfo destinationInfo = destinationProp.Find(item => item.Name == sourceProperty.Name);
                if (destinationInfo == null || Attribute.IsDefined(destinationInfo, typeof(MapIgnoreAttribute))) 
                {
                    continue;
                }
                try
                {
                    destinationInfo.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
                catch (Exception)
                {
                    // TODO : Propery map edilemediğinde alınacak aksiyon yazılır.
                }
            }
        }


        public static T MapToViewModel<T>(this IEntity source)
            where T : IModel
        {
            var destination = Activator.CreateInstance<T>();
            MatchProp(source, destination);
            return destination;
        }
        public static T MapToEntityModel<T>(this IModel source)
            where T : IEntity
        {
            var destination = Activator.CreateInstance<T>();
            MatchProp(source, destination);
            return destination;
        }
    }
}
