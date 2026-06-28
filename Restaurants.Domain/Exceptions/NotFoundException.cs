using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exceptions
{
    public class NotFoundException(string resourcetype, string resourceIdentifier) :
        Exception($"{resourcetype} with id: {resourceIdentifier} doesn't exist")
    {
        
    }
}
