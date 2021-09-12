using System;
using System.Runtime.Serialization;

namespace Weekly.API.Application.Commands
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        private Guid? id;

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Guid? id, Type entityType) : base($"Entity {entityType.Name} with id {id} could not be found.")
        {
            this.id = id;
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}