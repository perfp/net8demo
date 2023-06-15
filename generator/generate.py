from locust import HttpUser, between, task


class WebsiteUser(HttpUser):
    wait_time = between(5, 15)
    host = "http://localhost:5211"
    
    @task(10)
    def index(self):
        self.client.get("/todos")
        
    @task(100)
    def todo(self):
        self.client.get("/todos/1")