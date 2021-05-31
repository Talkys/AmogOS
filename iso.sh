#!/bin/sh
set -e
. ./build.sh

mkdir -p isodir
mkdir -p isodir/boot
mkdir -p isodir/boot/grub

cp sysroot/boot/amogos.kernel isodir/boot/amogos.kernel
cat > isodir/boot/grub/grub.cfg << EOF
menuentry "AmogOS" {
	multiboot /boot/amogos.kernel
}
EOF
grub-mkrescue -o amogos.iso isodir
