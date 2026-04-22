# Task Management API
Сервис регистрации задач

## Требования
- .NET 8 SDK
- PostgreSQL (локально или Docker)

## Быстрый старт

1. Установка PostgreSQL (Docker)
```bash
docker run --name postgres-task -e POSTGRES_PASSWORD=yourpassword -e POSTGRES_DB=JobDb -p 5432:5432 -d postgres
```

2. Настройка подключения

В appsettings.json укажите пароль:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=JobDb;Username=postgres;Password=yourpassword"
}
```

3. Запуск миграций

```bash
dotnet restore
dotnet ef database update
```

4. Запуск API

```bash
cd TaskManagement.Api
dotnet run
```
Swagger UI: https://localhost:7090/swagger

API Endpoints

· GET /api/tasks — все задачи
· GET /api/tasks/{id} — задача по ID
· POST /api/tasks — создать задачу
· PUT /api/tasks/{id} — обновить
· DELETE /api/tasks/{id} — удалить
· GET /api/tasks/type/{taskTypeId} — задачи по типу

## Описание архитектурных решений

### Чистая архитектура
- **Core** не зависит от внешних слоёв (нет EF, нет API)
- **Infrastructure** реализует интерфейсы
- **API** знает только Core + Infrastructure через DI

### Расширяемость
- `IRepository<T>` + `RepositoryBase<T>` — добавление новых сущностей без дублирования кода
- `IJobRepository` расширяет базовый репозиторий специфичными методами
- При добавлении нового типа задач достаточно добавить запись в `TaskType` — CRUD для задач не меняется

### Best practices
- Code-First + миграции
- Record DTOs
- Внедрение зависимостей
- Индексы в БД
- Предзаполнение данных
- CORS готов для будущего фронтенда
- Асинхронность (async/await)

### Выбор инструментов
| Компонент | Выбор | Почему |
|-----------|-------|--------|
| ORM | EF Core | Code-First, миграции, LINQ, интеграция с PostgreSQL |
| БД | PostgreSQL | Надёжность, JSON, расширяемость |
| API | Controllers | Гибкость, стандарт, удобный Swagger |
| Репозитории | Generic + специфичные | DRY + возможность расширения |
