# Juer.Tools
### Something tools for myself.
### 一个做给自己用的工具集合。
------

## 日志
2017/08/25 更新了对.ini文件操作的封装，操作更加直观一些。
```
//举个栗子
INIManager ini = new INIManager("./config.ini");
ini["section"]["key"] = "value";

Debug.WriteLine(ini["section"]["key"]); //Output : "value"
```
