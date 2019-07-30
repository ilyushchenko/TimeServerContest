# TimeServerContest

## Требования:
* .NET Core 2.2

## Запуск сервера:
1. Перейдите в папку *TimeServerContest* и откройте в ней консоль
2. Выполните команду **dotnet build**
3. Перейдите в папку **/bin/Debug/netcoreapp2.2**
4. Выполните команду **dotnet TimeServerContest.dll**

## Routes
/time - Возвращает текущее время UTC в формате JSON.
```
{
    "Time": "2019-07-30T19:23:35.7614891Z"
}
```
