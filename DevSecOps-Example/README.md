Software Composition Analysis:

dotnet list package --vulnerable

Static Application Security Testing (Semgrep)

docker run --rm -v "${PWD}:/src" returntocorp/semgrep semgrep scan --config "p/csharp" 

docker run --rm -v "${PWD}:/src" returntocorp/semgrep semgrep scan --config /src/SAST-Rules/sast-rules.yml

docker run --rm -v "${PWD}:/src" returntocorp/semgrep semgrep scan --config /src/SAST-Rules/sast-rules.yml --config "p/csharp" --config "p/security-audit"


docker run --rm -t zaproxy/zap-stable zap-api-scan.py -t "http://host.docker.internal:5000/openapi/v1.json" -f openapi


docker run --rm -v "${PWD}:/zap/wrk/:rw" -t zaproxy/zap-stable zap-api-scan.py -t "http://host.docker.internal:5176/openapi/v1.json" -f openapi -c "/zap/wrk/ZAP-Rules/zap-rules.conf"

