using UnityEngine;

public class UpdateUtils
{
    public enum UpdateType { Delta, UnscaledDelta, Fixed, UnscaledFixed}
    
    public static float UpdateBy(UpdateType type)
    {
        float updateMethod = 0;

        switch (type)
        {
            case UpdateType.Delta: updateMethod = Time.deltaTime; break;
            case UpdateType.UnscaledDelta: updateMethod = Time.unscaledDeltaTime; break;
            case UpdateType.Fixed: updateMethod = Time.fixedDeltaTime; break;
            case UpdateType.UnscaledFixed: updateMethod = Time.fixedUnscaledTime; break;
        }

        return updateMethod;
    }
}
