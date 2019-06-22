using Models;
using Prism.Events;

namespace Interfaces.Events
{
    public class RequestBodyDrawEvent : PubSubEvent<BodyModel>
    {
    }
}
