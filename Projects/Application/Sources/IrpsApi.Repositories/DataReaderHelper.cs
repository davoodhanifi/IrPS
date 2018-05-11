using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace Noandishan.IrpsApi.Repositories
{
    public static class DataReaderHelper
    {
        public static bool IsNull(this IDataReader reader, string name)
        {
            return reader[name] == DBNull.Value;
        }

        public static bool ReadBoolean(this IDataReader reader, string name)
        {
            return (bool)reader[name];
        }

        public static bool? ReadNullableBoolean(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (bool?)reader[name];
        }

        public static byte ReadByte(this IDataReader reader, string name)
        {
            return (byte)reader[name];
        }

        public static byte? ReadNullableByte(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (byte)reader[name];
        }

        public static byte[] ReadByteArray(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (byte[])reader[name];
        }

        public static int ReadInt32(this IDataReader reader, string name)
        {
            return (int)reader[name];
        }

        public static int? ReadNullableInt32(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (int)reader[name];
        }

        public static long ReadInt64(this IDataReader reader, string name)
        {
            return (long)reader[name];
        }

        public static long? ReadNullableInt64(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (long)reader[name];
        }

        public static decimal ReadDecimal(this IDataReader reader, string name)
        {
            return (decimal)reader[name];
        }

        public static decimal? ReadNullableDecimal(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (decimal)reader[name];
        }

        public static string ReadString(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return (string)reader[name];
        }

        public static ISet<string> ReadStringSet(this IDataReader reader, string name, string separator)
        {
            if (reader[name] == DBNull.Value)
                return new HashSet<string>();

            var value = (string)reader[name];

            return new HashSet<string>(Split(value, separator), StringComparer.OrdinalIgnoreCase);
        }

        public static IList<string> ReadStringList(this IDataReader reader, string name, string separator = ",")
        {
            if (reader[name] == DBNull.Value)
                return new List<string>();

            var value = (string)reader[name];

            return new List<string>(Split(value, separator));
        }

        //public static JalaliDateTime ReadJalaliDateTime(this IDataReader reader, string name)
        //{
        //    if (reader[name] == DBNull.Value)
        //        return null;

        //    return new JalaliDateTime(((long)reader[name]));
        //}

        public static Version ReadVersion(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return null;

            return new Version((string)reader[name]);
        }

        public static T ReadEnum<T>(this IDataReader reader, string name) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), reader.ReadInt32(name));
        }

        public static T ReadNullableEnum<T>(this IDataReader reader, string name) where T : struct
        {
            var value = reader.ReadNullableInt32(name);

            if (value == null)
                return default(T);

            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ReadJson<T>(this IDataReader reader, string name)
        {
            if (reader[name] == DBNull.Value)
                return default(T);

            return JsonConvert.DeserializeObject<T>((string)reader[name]);
        }

        //public static T ReadCompressedJson<T>(this IDataReader reader, string name)
        //{
        //    if (reader[name] == DBNull.Value)
        //        return default(T);

        //    using (var stream = new MemoryStream((byte[])reader[name]))
        //    using (var zipStream = new GZipStream(stream, CompressionMode.Decompress))
        //    using (var streamReader = new StreamReader(zipStream))
        //        return JSON.Deserialize<T>(streamReader);
        //}

        //public static TEntity ReadEntity<TEntity>(this IDataReader reader, string name, IEntityRepository<TEntity> repository) where TEntity : IEntity
        //{
        //    if (reader[name] == DBNull.Value)
        //        return default(TEntity);

        //    return repository.Get((int)reader[name], EntityGetOptions.Lazy);
        //}

        //public static IEnumerable<TEntity> ReadEntities<TEntity>(this IDataReader reader, string name, IEntityRepository<TEntity> repository, string separator = ",") where TEntity : IEntity
        //{
        //    if (reader[name] == DBNull.Value)
        //        return Enumerable.Empty<TEntity>();

        //    var value = (string)reader[name];

        //    return Split(value, separator).Select(id => repository.Get(int.Parse(id), EntityGetOptions.Lazy));
        //}

        //public static object ReadVariant(this IDataReader reader, string name)
        //{
        //    if (!reader.IsNull($"Boolean{name}"))
        //        return reader.ReadBoolean($"Boolean{name}");

        //    if (!reader.IsNull($"Integer{name}"))
        //        return reader.ReadInt64($"Integer{name}");

        //    if (!reader.IsNull($"Decimal{name}"))
        //        return reader.ReadDecimal($"Decimal{name}");

        //    if (!reader.IsNull($"Text{name}"))
        //        return reader.ReadString($"Text{name}");

        //    if (!reader.IsNull($"Date{name}"))
        //        return reader.ReadJalaliDateTime($"Date{name}");

        //    if (!reader.IsNull($"Binary{name}"))
        //        return reader.ReadByteArray($"Binary{name}");

        //    return null;
        //}

        private static IEnumerable<string> Split(string value, string separator)
        {
            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}