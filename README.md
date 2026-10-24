# TailorWebsite
This is my engineering thesis project. It is a website for a tailoring service that includes basic information about the workshop as well as a set of features designed to facilitate the shop’s operations.

## Uruchomienie w Dockerze

1. Skopiuj `.env.example` do `.env` i ustaw prawidłowy `DB_CONNECTION_STRING`.
2. Upewnij się, że istnieją zewnętrzne sieci Dockera `db-network` i `proxy-network`.
3. Uruchom aplikację:

```powershell
docker network create db-network
docker network create proxy-network
docker compose up --build
```

Aplikacja jest dostępna pod adresem `http://localhost:8785`, a endpoint
`/health` zwraca status procesu kontenera. Podczas startu aplikacja wykonuje
migracje bazy danych oraz inicjalizuje role.
