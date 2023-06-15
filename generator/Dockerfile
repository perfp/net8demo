FROM python:3

WORKDIR /app
ADD generate.py /app/
COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt
ENTRYPOINT [ "locust", "-f", "generate.py" ]
EXPOSE 8089
