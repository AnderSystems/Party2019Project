using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

public static class NetUltility
{
    public static string GetIPV4()
    {
        string ipv4 = null;

        IPAddress[] iPAddresses = Dns.GetHostAddresses(Dns.GetHostName());

        foreach(IPAddress ip4 in iPAddresses.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
        {
            ipv4 = ip4.ToString();
        }

        return ipv4;

    }
}

public static class Vectors
{
    public static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        Vector3 Result = new Vector3();

        Result.x = a.x * b.x;
        Result.y = a.y * b.y;
        Result.z = a.z * b.z;

        return Result;
    }

    public static Vector3 Add(Vector3 a, Vector3 b)
    {
        Vector3 Result = new Vector3();

        Result.x = a.x + b.x;
        Result.y = a.y + b.y;
        Result.z = a.z + b.z;

        return Result;
    }

    public static Vector3 Subtract(Vector3 a, Vector3 b)
    {
        Vector3 Result = new Vector3();

        Result.x = a.x - b.x;
        Result.y = a.y - b.y;
        Result.z = a.z - b.z;

        return Result;
    }
}

public static class Quaternions
{
    public static Quaternion Subtract(Quaternion a, Quaternion b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x - b.eulerAngles.x;
        VctResult.y = a.eulerAngles.y - b.eulerAngles.y;
        VctResult.z = a.eulerAngles.z - b.eulerAngles.z;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
    public static Quaternion Subtract(Quaternion a, float b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x - b;
        VctResult.y = a.eulerAngles.y - b;
        VctResult.z = a.eulerAngles.z - b;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
    public static Quaternion Add(Quaternion a, Quaternion b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x + b.eulerAngles.x;
        VctResult.y = a.eulerAngles.y + b.eulerAngles.y;
        VctResult.z = a.eulerAngles.z + b.eulerAngles.z;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
    public static Quaternion Add(Quaternion a, float b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x + b;
        VctResult.y = a.eulerAngles.y + b;
        VctResult.z = a.eulerAngles.z + b;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
    public static Quaternion Multiply(Quaternion a, Quaternion b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x * b.eulerAngles.x;
        VctResult.y = a.eulerAngles.y * b.eulerAngles.y;
        VctResult.z = a.eulerAngles.z * b.eulerAngles.z;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
    public static Quaternion Multiply(Quaternion a, float b)
    {
        Quaternion Result = new Quaternion();
        Vector3 VctResult = new Vector3();
        VctResult.x = a.eulerAngles.x - b;
        VctResult.y = a.eulerAngles.y - b;
        VctResult.z = a.eulerAngles.z - b;
        Result = Quaternion.Euler(VctResult);

        return Result;
    }
}

public class VectorExtraCalcules : MonoBehaviour
{
    Vector3 a;
    Vector3 b;

    private void Start()
    {
        //a - b;
    }
}
