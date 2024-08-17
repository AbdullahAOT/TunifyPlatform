# Introduction
Tunify Platform is a web application designed for music enthusiasts, allowing users to manage playlists, explore songs, and interact with various music-related content. The platform supports features such as user subscriptions, playlist management, song curation, and artist and album exploration. Tunify Platform leverages a robust relational database to manage and store data efficiently, ensuring a seamless user experience.

# Overview of Relationships

# Users and Subscriptions
One-to-One Relationship: Each user has one subscription plan. This relationship is represented by the User and Subscription entities.

# Users and Playlists
One-to-Many Relationship: A single user can create multiple playlists, but each playlist is associated with only one user.

# Playlists and Songs
Many-to-Many Relationship: A playlist can contain multiple songs, and a song can be part of multiple playlists. This is managed through the PlaylistSongs join entity.

# Artists and Albums
One-to-Many Relationship: An artist can produce multiple albums, but each album is linked to a single artist.

# Albums and Songs
One-to-Many Relationship: Each album consists of multiple songs, but each song is associated with a single album.

This structure allows Tunify Platform to efficiently manage user-generated content and music-related data, ensuring that users can create personalized playlists and explore a vast library of songs, artists, and albums.