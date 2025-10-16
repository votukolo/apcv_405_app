pipeline {
    agent any

    environment {
        IMAGE_NAME = "votukolo/apcv_405_app"
    }

    stages {
        stage('Checkout') {
            steps {
                // This clones the current repository Jenkins is building
                checkout scm
            }
        }

        stage('Run Tests') {
            steps {
                bat 'dotnet test'
            }
        }

        stage('Build Docker Image') {
            steps {
                bat 'docker build -t  votukolo/apcv_405_app .'
            }
        }

        stage('Push Docker Image to DockerHub') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    bat 'echo %DOCKER_PASS% | docker login -u %DOCKER_USER% --password-stdin'
                    bat 'docker push votukolo/apcv_405_app'

                }
            }
        }

        stage('Run Docker Container') {
            steps {
                bat 'docker run -d -p 9090:8080 --name app_container votukolo/apcv_405_app'
            }
        }

        stage('Health Check') {
            steps {
                script {
                    sleep(5) // Give container time to start
                    def status = powershell(
                    script: '$r = Invoke-WebRequest -Uri http://localhost:9090/health -UseBasicParsing -TimeoutSec 5; $r.StatusCode',
                    returnStdout: true
                    ).trim()
                    if (status != '200') {
                    error("Health check failed. Status code: ${status}")
                    } else {
                    echo "Health check passed ✅"
                    }
            }
       }
    }
        post {
        always {
            // Clean up Docker container to avoid port conflict
            bat 'docker rm -f app_container || true'
        }
    }
}












