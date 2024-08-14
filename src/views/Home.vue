<template>
  <v-container>
    <v-row>
      <v-col>
        <v-img src="/cover-image.jpg" alt="Order Management" class="cover-image"></v-img>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'Home',
};
</script>

<style scoped>
.cover-image {
  width: 100%;
  height: 300px;
  object-fit: cover;
}
</style>
