using Unity.Transforms;
using UnityEngine;

public static class DataExtention
{
    public static T ToDeserialized<T>(this string json) =>
           JsonUtility.FromJson<T>(json);

    public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

    public static LocalTransformData ToLocalTranformData(this LocalTransform localTransform) =>
        new LocalTransformData(localTransform.Position, localTransform.Rotation, localTransform.Scale);

    public static LocalTransform ToLocalTranform(this LocalTransformData localTransformData) =>
        new LocalTransform
        {
            Position = localTransformData.Position,
            Rotation = localTransformData.Rotation,
            Scale = localTransformData.Scale
        };
}
