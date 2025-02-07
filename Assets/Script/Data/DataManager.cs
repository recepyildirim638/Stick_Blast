using System.Collections;
using System.Collections.Generic;

public class DataManager : BaseGameData
{
    public void Initalize() => LoadGameData();

    public MainData GetMainData() => this.mainData;
}
