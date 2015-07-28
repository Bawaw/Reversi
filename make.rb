#!ruby

ID = 'reversi'
ZIP_FILE = "#{ID}.zip"

$:.unshift( ENV['KHL_ROOT_DIRECTORY'] + '/shared' )
require 'shared.rb'


def package
  files = `git ls-files`.lines.map do |file|
    file.strip
  end

  zip( ZIP_FILE, files )
end

def upload
  remote_directory = "/var/www/VGO/reversi/"

  remote do |scp|
    scp.upload(ZIP_FILE, remote_directory)
  end
end

commands do
  command 'zip' do
    action do
      package
    end
  end

  command 'upload' do
    action do
      package
      upload
    end
  end
end
