# Angular Building

## AWS CodeBuild

### buildspec.yml

The recommended location for build and deployment scripts is the *build* directory that sits at the root of the repository. As there could be different buildspec.yml files for different CodeBuild deployments from the repository, it is best that the Angular buildspec has its own specific name. A possible name for this is simply *buildspec-angular.yml*. The build steps in this buildspec file will need to be adjusted to point to where the Angular code is located in the repository structure.

#### Build and Test Locally

Requirements:
 - PowerShell
 - Docker

On a Windows system the buildspec.yml file can be built and tested locally by using the CodeBuild Agent.
To do this go [here](https://hub.docker.com/r/amazon/aws-codebuild-local/) and follow the instructions to get aws-codebuild-local.

To verify the download using the SHA signature go here: https://docs.aws.amazon.com/codebuild/latest/userguide/use-codebuild-agent.html

Download the codebuild.sh script to the Angular directory.

To run the CodeBuild Agent locally, open PowerShell and go to the Angular directory (Shift+Right Click and click Open PowerShell window here).

The CodeBuild Agent is run inside a Docker container. Pre-built containers specs can be found in the aws git repository https://github.com/aws/aws-codebuild-docker-images.git so check this out and select an environment, then build it using the following:

```
docker build -f "Path\to\Dockerfile" -t docker_image_name[:optional_tag] "Path\To\Build\Directory"
or
docker build -f "Path\to\Dockerfile" -t docker_image_name:optional_tag .
```

To explore the docker image find the associated Image Id by running `docker images` in PowerShell, then `docker run -it <Image Id>`.

To build the Angular code run the following
```
./codebuild_build.sh -i docker_image_name[:optional_tag] -a /dist
```


Links:
 - https://aws.amazon.com/blogs/devops/announcing-local-build-support-for-aws-codebuild/
 - https://docs.aws.amazon.com/codebuild/latest/userguide/use-codebuild-agent.html
 - https://github.com/aws/aws-codebuild-docker-images/tree/master/local_builds
