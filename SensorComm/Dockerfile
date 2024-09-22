# Используем базовый образ Ubuntu
FROM ubuntu:latest

# Устанавливаем необходимые зависимости
RUN apt-get update && apt-get install -y g++ make

# Копируем все файлы проекта в контейнер
COPY . /app
RUN ls -la /app
# Устанавливаем рабочую директорию
WORKDIR /app

# Выполняем компиляцию приложения\
RUN make

# Запускаем приложение SensorComm
CMD ["./SensorComm"]
