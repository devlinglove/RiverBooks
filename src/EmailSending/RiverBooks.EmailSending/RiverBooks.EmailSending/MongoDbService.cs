using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.EmailSending
{
	internal class MongoDbService : IOutboxService
	{
		private readonly IMongoCollection<EmailOutboxEntity> _mongoCollection;

		public MongoDbService(IMongoCollection<EmailOutboxEntity> mongoCollection)
		{
			_mongoCollection = mongoCollection;
		}

		public async Task QueEmailForSending(EmailOutboxEntity entity)
		{
			await _mongoCollection.InsertOneAsync(entity);	
		}
	}
}
