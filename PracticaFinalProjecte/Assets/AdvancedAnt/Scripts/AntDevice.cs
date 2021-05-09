
using System.Collections;
using ANT_Managed_Library;

public class AntDevice  {

    public string name;
    public byte deviceType;
    public byte transType;
    public int period;
    public int deviceNumber;
    public int radiofreq;

    override
    public string ToString()
    {
        return name + " " + deviceType + " " + transType + " " + period + " " + deviceNumber + " " + radiofreq;
    }
}
