using UnityEngine;

public class PathBuilder
{
    private Path _path;
    
    public void CreateNewPath()
    {
        _path = new Path();
    }
    
    public void CreatePathPoint(Vector2 screenCoords)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(screenCoords);
        point.y = 0;
        _path.AddPoint(point);
    }

    public Path GetPath() => _path;
}
