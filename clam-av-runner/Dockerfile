ADD file ... in /
CMD ["/bin/sh"]
LABEL maintainer=https://mko-x.github.io/docker-clamav/
/bin/sh -c apk add --no-cache
COPY dir:e14b3e7e8df20b75163fad3273d603b99eca7b6e1ca9be0d8357187993059764 in /etc/clamav
COPY file:22d3cad6a9f1a7c6927183d393f904affffa7a76a6065b30645dfa78a8d72e23 in /
COPY file:d3be5ab7fcb86b91c7b7787750bb47688cf0bc0c0327e8b5014f9529332c3b6b in /check.sh
EXPOSE 3310/tcp
VOLUME [/store]
CMD ["/bootstrap.sh"]
HEALTHCHECK &{["CMD-SHELL" "/check.sh"] "0s"
