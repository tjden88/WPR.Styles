﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace WPR.Tools
{
    /// <summary>
    /// Операции записи, чтения и копирования объектов с использованием System.Text.Json
    /// </summary>
    public static class DataSerializer
    {
        /// <summary> Последняя отловленная ошибка при операции </summary>
        public static Exception LastOperationException { get; private set; }

        /// <summary>
        /// Создать копию объекта
        /// </summary>
        /// <param name="obj">Исходный объект</param>
        /// <returns>default, если не удалось</returns>
        public static T CopyObject<T>(T obj)
        {
            try
            {
                var serialized = JsonSerializer.Serialize(obj);
                return JsonSerializer.Deserialize<T>(serialized);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "DataSerializer");
                LastOperationException = e;
                return default;
            }
        }

        /// <summary>
        /// Сохранить объект в файл
        /// </summary>
        /// <param name="obj">Исходный объект</param>
        /// <param name="FileName">Имя файла</param>
        /// <returns>false, если сохранение не удалось</returns>
        public static bool SaveToFile<T>(T obj, string FileName)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, };
                var serialized = JsonSerializer.Serialize(obj, options);

                File.WriteAllText(FileName, serialized, Encoding.UTF8);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "DataSerializer");
                LastOperationException = e;
            }
            return false;
        }

        /// <summary>
        /// Загрузить из файла
        /// </summary>
        /// <param name="FileName">Имя файла</param>
        /// <returns>default, если не удалось</returns>
        public static T LoadFromFile<T>(string FileName)
        {
            if (!File.Exists(FileName))
                return default;
            try
            {
                var jsonString = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "DataSerializer");
                LastOperationException = e;
                return default;
            }
        }
    }
}