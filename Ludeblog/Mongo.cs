using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Nancy;

namespace Ludeblog {
	public static class Mongo {
		private static IMongoClient client = InitClient();
		private static Dictionary<String, IMongoDatabase> databases = InitDatabases();

		/**
		 * Initialize Mongo Client
		 */
		private static IMongoClient InitClient() {
			return new MongoClient("mongodb://localhost");
		}

		/**
		 * Initialize Mongo Databases
		 */
		private static Dictionary<String, IMongoDatabase> InitDatabases() {
			databases = new Dictionary<string, IMongoDatabase>();

			// TODO: Allow setup from config instead of hardcoded
			databases["john"] = client.GetDatabase("john");
			databases["clinton"] = client.GetDatabase("clinton");
			//databases["clint"] = client.GetDatabase("clint");
			//databases["danee"] = client.GetDatabase("danee");

			return databases;
		}

		public static async Task Insert(NancyContext context, String collectionName, BsonDocument document) {
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (collectionName == null) throw new ArgumentNullException(nameof(collectionName));
			if (document == null) throw new ArgumentNullException(nameof(document));

			var collection = Database(context).GetCollection<BsonDocument>(collectionName);

			await collection.InsertOneAsync(document);
		}

		public static async Task<List<BsonDocument>> Find(NancyContext context, String collectionName, BsonDocument filter) {
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (collectionName == null) throw new ArgumentNullException(nameof(collectionName));
			if (filter == null) throw new ArgumentNullException(nameof(filter));

			var collection = Database(context).GetCollection<BsonDocument>(collectionName);

			if (filter == null) filter = new BsonDocument();

			return await collection.Find(filter).ToListAsync();
		}

		public static async Task<UpdateResult> Update(NancyContext context, String collectionName, BsonDocument filter, BsonDocument update) {
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (collectionName == null) throw new ArgumentNullException(nameof(collectionName));
			if (filter == null) throw new ArgumentNullException(nameof(filter));
			if (update == null) throw new ArgumentNullException(nameof(update));

			var collection = Database(context).GetCollection<BsonDocument>(collectionName);

			return await collection.UpdateManyAsync(filter, update);
		}

		public static async Task<DeleteResult> Remove(NancyContext context, String collectionName, BsonDocument filter) {
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (collectionName == null) throw new ArgumentNullException(nameof(collectionName));
			if (filter == null) throw new ArgumentNullException(nameof(filter));

			var collection = Database(context).GetCollection<BsonDocument>(collectionName);

			return await collection.DeleteManyAsync(filter);
		}

		/**
		 * Return proper database based on context
		 */
		private static IMongoDatabase Database(NancyContext context) {
			return databases[context.Parameters.subdomain];
		}
	}
}
