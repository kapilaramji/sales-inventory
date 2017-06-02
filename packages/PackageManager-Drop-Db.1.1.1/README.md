A very simple nuget package that - when installed into Visual Studio - will add a `Drop-Db` command into the Visual Studio package manager.

Usage:

```powershell
Drop-Db MyDb
```


```powershell
Drop-Db MyDb -ServerInstance '.\SQLEXPRESS'
```

When used with localdb will first stop the database in order to ensure the operation completes

