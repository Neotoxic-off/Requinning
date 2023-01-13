# Requinning
🐝 Obfuscator For Reverse Engineering Evasion

### Obfuscation
#### Clear
- Assembly

#### Rename
- Rename Module
- Rename Methods
- Rename Parameters
- Rename Events
- Rename Fields

#### Remove
- Ctor

#### Spoof
- GUID

### Example
- Before
```C#
private void LoadAssembly()
{
	bool flag = File.Exists(this.path);
	if (flag)
	{
		this.Logger.Record("Loading file assembly...");
		this.Module = ModuleDefMD.Load(this.path, null);
		this.Module.Name = this.protections.Rename(this.Module.Name);
		this.Logger.Record("File assembly loaded");
	}
	else
	{
		this.Logger.Record("File not found '" + this.path + "'");
	}
}
```

- After
```C#
private void \ref1\re74\red4\re32\rece\re4f\rec9\re94\recb\re65\re5f\re0c\read\re6d\re2a\re0d\reb8\re77\re9e\ree3\reb2\re4a\re67\re17\re7f\re15\recb\rece\re3b\re79\re65\reae()
{
	bool flag = File.Exists(this.path);
	if (flag)
	{
		this.Logger.\rebf\redd\re51\re06\re98\reef\re3c\recb\ref1\re7f\re2c\re67\refc\re47\re79\re0e\re1b\rea7\re28\re94\refb\re74\refe\re28\re4b\rebc\re13\re6b\re47\reef\re73\ree6("Loading file assembly...");
		this.Module = ModuleDefMD.Load(this.path, null);
		this.Module.Name = this.protections.\re30\re64\red7\re9a\re29\re5c\reef\ree4\reb3\re84\reb8\refe\re5c\reb2\re9f\red6\reb0\re6d\re7e\reb1\read\re94\reb8\re89\re5f\re41\re48\re03\re9d\re06\reb3\re30(this.Module.Name);
		this.Logger.\rebf\redd\re51\re06\re98\reef\re3c\recb\ref1\re7f\re2c\re67\refc\re47\re79\re0e\re1b\rea7\re28\re94\refb\re74\refe\re28\re4b\rebc\re13\re6b\re47\reef\re73\ree6("File assembly loaded");
	}
	else
	{
		this.Logger.\rebf\redd\re51\re06\re98\reef\re3c\recb\ref1\re7f\re2c\re67\refc\re47\re79\re0e\re1b\rea7\re28\re94\refb\re74\refe\re28\re4b\rebc\re13\re6b\re47\reef\re73\ree6("File not found '" + this.path + "'");
	}
}
```
