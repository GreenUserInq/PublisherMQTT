# ���������� ������� ����� Ubuntu
FROM ubuntu:latest

# ������������� ����������� �����������
RUN apt-get update && apt-get install -y g++ make

# �������� ��� ����� ������� � ���������
COPY . /app
RUN ls -la /app
# ������������� ������� ����������
WORKDIR /app

# ��������� ���������� ����������\
RUN make

# ��������� ���������� SensorComm
CMD ["./SensorComm"]
