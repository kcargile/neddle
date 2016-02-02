FROM phusion/passenger-nodejs:0.9.18
MAINTAINER Kristopher Cargile "kristopher@cargiletech.com"

ENV HOME=/root \
	TERM=xterm

CMD ["/sbin/my_init"]

WORKDIR /home/app/webapp

RUN apt-get update && \
	apt-get install -y --no-install-recommends \
	nano

COPY .build/nginx/ /etc/neddle.d/
COPY .build/env.conf /etc/neddle.d/
COPY . ./

RUN	npm install

RUN ln -sf /etc/neddle.d/env.conf /etc/nginx/main.d/neddle.env.conf && \
	rm -f /etc/service/nginx/down && \
	rm -rf ./.build && \
	apt-get clean && \
	rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

EXPOSE 8000