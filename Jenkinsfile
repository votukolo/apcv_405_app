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
                sh 'dotnet test ClassProjectApp.Tests/ClassProjectApp.Tests.csproj'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t $IMAGE_NAME .'
            }
        }

        stage('Push Docker Image to DockerHub') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'dockerhub-creds', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    sh """
                        echo "$DOCKER_PASS" | docker login -u "$DOCKER_USER" --password-stdin
                        docker push $IMAGE_NAME
                    """
                }
            }
        }

        stage('Run Docker Container') {
            steps {
                sh 'docker run -d -p 9090:8080 --name app_container $IMAGE_NAME'
            }
        }

        stage('Health Check') {
            steps {
                script {
                    sleep(5) // Give container time to start
                    def response = sh(script: 'curl -s -o /dev/null -w "%{http_code}" http://localhost:9090/health', returnStdout: true).trim()
                    if (response != '200') {
                        error("Health check failed. Status code: ${response}")
                    } else {
                        echo "Health check passed ✅"
                    }
                }
            }
        }
    }

    post {
        always {
            // Clean up Docker container to avoid port conflict
            sh 'docker rm -f app_container || true'
        }
    }
}
