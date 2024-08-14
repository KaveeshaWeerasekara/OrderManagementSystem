<template>
  <v-app>
    <v-app-bar app>
      <v-toolbar-title>Order Management System</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-tabs>
        <v-tab to="/">Home</v-tab>
        <v-tab to="/users">Users</v-tab>
        <v-tab to="/products">Products</v-tab>
        <v-tab to="/orders">Orders</v-tab>
      </v-tabs>
    </v-app-bar>
    
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
/* Add any global styles here */
</style>
