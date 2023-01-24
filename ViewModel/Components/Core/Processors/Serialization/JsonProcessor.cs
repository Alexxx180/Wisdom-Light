﻿using System.IO;
using Newtonsoft.Json;
using WisdomLight.ViewModel.Customing;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.Model.Exceptions.Json;
using System.Collections.Generic;
using System;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Handling;
using WisdomLight.ViewModel.Components.Data.Units.Fields;

namespace WisdomLight.ViewModel.Components.Core.Processors.Serialization
{
    internal class JsonProcessor : FileProcessor
    {
        private const string Extension = "json";
        private static readonly JsonSerializer _serializer;

        static JsonProcessor()
        {
            _serializer = new JsonSerializer()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesBinder
                {
                    KnownTypes = new List<Type>
                    {
                        typeof(TextExpression),
                        typeof(NumberExpression),
                        typeof(DateExpression)
                    }
                }
            };
        }

        public override string FixExtension(string path)
        {
            return Path.GetExtension(path).ToLower() == $".{Extension}" ? path : path.ToFile(Extension);
        }

        /// <summary>
        /// Rename JSON files on the same file path
        /// </summary>
        /// <param name="path">Path to files</param>
        /// <param name="original">Original file name</param>
        /// <param name="next">New file name</param>
        /// <exception cref="MoveException">Renaming failure</exception>
        internal static new void Rename(string path, string original, string next)
        {
            FileProcessor.Rename(path, original.ToFile(Extension), next.ToFile(Extension));
        }

        /// <summary>
        /// Deserialize object from JSON file
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <exception cref="JsonParseException">JSON Parsing failure</exception>
        /// <exception cref="ReadException">Reading failure</exception>
        protected internal override T Read<T>(string path)
        {
            T deserilizeable;

            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    deserilizeable = (T)_serializer.Deserialize(new JsonTextReader(file), typeof(T));
                }
            }
            catch (JsonException exception)
            {
                throw new JsonParseException(exception, path);
            }
            catch (IOException exception)
            {
                throw new ReadException(exception, path);
            }

            return deserilizeable;
        }

        /// <summary>
        /// Serialize object into JSON file
        /// </summary>
        /// <param name="path">Full path to file</param>
        /// <param name="serilizeable">Serializeable object</param>
        /// <exception cref="SaveException">Saving failure</exception>
        protected internal override void Write<T>(string path, T serilizeable)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                _serializer.Serialize(file, serilizeable);
            }
        }
    }
}