namespace Core.Meta.Analytics
{
    public static class AnalyticsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">class type</typeparam>
        /// <returns></returns>
        public static string GetMemberKey<T>(this object variable,string nameOfVariable)
        {
            return $"{typeof(T).FullName}.{nameOfVariable}";
        }
        
        public static string GetMemberKey<T>(string nameOfVariable)
        {
            return $"{typeof(T).FullName}.{nameOfVariable}";
        }
    }
}