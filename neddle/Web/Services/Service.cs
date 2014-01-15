using System.Reflection;
using log4net;

namespace Neddle.Web.Services
{
    /// <summary>
    /// A basic REST web service.
    /// </summary>
    public abstract class Service
    {
        /// <summary>
        /// The current active ILog instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default service namespace.
        /// </summary>
        public const string DefaultNamespace = "http://www.neddle.org/2012/08";

        /// <summary>
        /// Acknowledges the service is listening.
        /// </summary>
        /// <returns></returns>
        public abstract string Ack();

        /// <summary>
        /// Formats and returns an acknowledgement message.
        /// </summary>
        /// <returns>A string containing an acknowledgement message.</returns>
        protected virtual string AckInternal()
        {
            return string.Format("{0} is listening.", GetType().Name);
        }
    }
}
